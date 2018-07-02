using CitizenFX.Core;
using System;
using System.Collections.Generic;

namespace DC
{
    public class HiveServer : BaseScript
    {
        private readonly Dictionary<string, HiveUser> users = null;
        private bool isReady = false;        

        private readonly Action onMySQLReadyDelegate = null;

        public HiveServer()
        {
            users = new Dictionary<string, HiveUser>();

            onMySQLReadyDelegate = new Action(OnMySQLReady);
            EventHandlers["onMySQLReady"] += onMySQLReadyDelegate;

            EventHandlers[HiveShared.EventRegisterUser] += new Action<Player>(RegisterUser);
        }

        private void RegisterUser([FromSource]Player player)
        {
            Ready(() =>
            {
                string steamid = player.Identifiers["steam"];
                Exports["mysql-async"].mysql_fetch_all("SELECT * FROM users WHERE steamid = @steamid;",
                    new Dictionary<string, object> {{"@steamid", steamid}},
                    new Action<List<object>> ((List<object> result) =>
                    {              
                        if (result.Count > 0)
                        {
                            // user already exists in the table
                            var expando = (dynamic)result[0];
                            HiveUser user = NewUser(expando.name, expando.steamid, expando.license, expando.ip);
                            player.TriggerEvent(HiveShared.EventRegisterUserResponse, user);
                        }
                        else
                        {
                            // new user added to table
                            string license = player.Identifiers["license"];
                            string ip = player.Identifiers["ip"];

                            Exports["mysql-async"].mysql_insert("INSERT INTO users VALUES (@name, @steamid, @license, @ip);",
                            new Dictionary<string, object> {
                                {"@name", player.Name},
                                {"@steamid", steamid},
                                {"@license", license},
                                {"@ip", ip}
                            }, new Action(() => {
                                HiveUser user = NewUser(player.Name, steamid, license, ip);
                                player.TriggerEvent(HiveShared.EventRegisterUserResponse, user);
                            }), false);
                        }
                    }), false);
            });           
        }

        private HiveUser NewUser(string name, string steamid, string license, string ip)
        {
            HiveUser user = new HiveUser()
            {
                Name = name,
                SteamId = steamid,
                License = license,
                Ip = ip
            };
            users[steamid] = user;
            return user;
        }

        private void Ready(Action callback)
        {
            if (!isReady)
            {
                Action lambda = null;
                lambda = () => {
                    EventHandlers["onMySQLReady"] -= lambda;
                    callback();
                };
                EventHandlers["onMySQLReady"] += lambda;
            }                
            else
                callback();
        }

        private void OnMySQLReady()
        {
            EventHandlers["onMySQLReady"] -= onMySQLReadyDelegate;
            isReady = true;            
        }
    }    
}
