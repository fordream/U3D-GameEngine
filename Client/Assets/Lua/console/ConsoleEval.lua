local Console = require "console.Console"

local ConsoleEval = class(Console)

function ConsoleEval:name () 
	return "EVAL"
end
function ConsoleEval:description()
	return "Function eval"
end
function ConsoleEval:usage()
	return "eval print('hello world')"
end
function ConsoleEval:execute()
	return function (args)
		if #args == 0 then
			return self:ShowUsage()
		else
			Slua.ldb.printExpr(args[1])
		end
	end
end

RefisterConsoleCommand(ConsoleEval)