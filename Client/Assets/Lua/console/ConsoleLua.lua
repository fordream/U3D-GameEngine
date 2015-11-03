
local ConsoleLua = class()

function ConsoleLua:name () 
	return "lua"
end
function ConsoleLua:description()
	return "execute lua string"
end
function ConsoleLua:usage()
	return "lua print('hello ')"
end
function ConsoleLua:showUsage()
	local fmt = [[<b>%s Command</b>
    <b>Description:</b> %s
    <b>Usage:</b> %s]]
    local usage = string.format(fmt,self:name(),self:description(),self:usage())
    UnityEngine.Debug.Log(usage)
end
function ConsoleLua:execute(args)
	if #args == 0 then
		self:showUsage()
	else
		-- run script
		local script = table.concat( args, " ")
		local f, err = loadstring(script)
		if f == nil then
			warn(err)
		else
			f()
		end
	end
end

RegCommand(ConsoleLua())