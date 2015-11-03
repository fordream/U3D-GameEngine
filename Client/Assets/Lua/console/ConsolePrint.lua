
local ConsolePrint = class()

function ConsolePrint:name () 
	return "print"
end
function ConsolePrint:description()
	return "Function print"
end
function ConsolePrint:usage()
	return "print hello world"
end
function ConsolePrint:showUsage()
	local fmt = [[<b>%s Command</b>
    <b>Description:</b> %s
    <b>Usage:</b> %s]]
    local usage = string.format(fmt,self:name(),self:description(),self:usage())
    UnityEngine.Debug.Log(usage)
end
function ConsolePrint:execute(args)
	if #args == 0 then
		self:showUsage()
	else
		print(table.concat( args, ", "))
	end
end

RegCommand(ConsolePrint())
