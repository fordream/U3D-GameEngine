import "SLua"

local function logCommand(cmd)
	UnityEngine.Debug.Log("> " .. cmd)
end

local Commands = {}
function RegCommand(command)
	Commands[command:name()] = command
end
local function Execute(cmd,args)
	if Commands[cmd] ~= nil then
		Commands[cmd]:execute(args)
	else
		UnityEngine.Debug.Log("Command <color=red>" .. cmd .. "</color> not found.")
	end
end

local function onExecuteCommand(input)
	local args = string.split(input, ' ')
	local cmd = table.remove(args,1)
	logCommand(cmd)
	Execute(cmd,args)
end

Wenzil.Console.LuaCommandExecute.onExecuteCommand = onExecuteCommand



