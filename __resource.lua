client_script {
    "DCConfigParser/DCConfigParserShared.net.dll",
    "DCDisableDispatch/DCDisableDispatchClient.net.dll",
    "DCRemoveCops/DCRemoveCopsShared.net.dll",
    "DCRemoveCops/DCRemoveCopsClient.net.dll",    
    "DCStreetName/DCStreetNameClient.net.dll"
}
server_script {
    "DCConfigParser/DCConfigParserShared.net.dll",
    "DCRemoveCops/DCRemoveCopsShared.net.dll",
    "DCRemoveCops/DCRemoveCopsServer.net.dll"
}
file {
    "DCLoadScreen/index.html",
    "DCRemoveCops/config.ini"
}
loadscreen "DCLoadScreen/index.html"