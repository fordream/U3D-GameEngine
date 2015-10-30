import "UnityEngine"

local function createUIRoot()
	print("create uiroot")
	local uiRoot = GameObject("Canvas(UIRoot)");
    uiRoot.layer = UnityEngine.LayerMask.NameToLayer("UI");
    local canvas = uiRoot:AddComponent(Canvas);
    canvas.renderMode = UnityEngine.RenderMode.ScreenSpaceOverlay;

    uiRoot:AddComponent(UI.CanvasScaler);
    uiRoot:AddComponent(UI.GraphicRaycaster);


    local eventObj = GameObject("EventSystem");
    local eventSystem = eventObj:AddComponent(EventSystems.EventSystem);

    eventObj:AddComponent(EventSystems.StandaloneInputModule);
    eventObj:AddComponent(EventSystems.TouchInputModule);

    return uiRoot
end

local function loadRes(url,cb)
	local c = coroutine.create(function()
        -- Yield(WaitForSeconds(2))
        -- print "coroutine WaitForSeconds 2"
        print("loadRes url=",url)
        local www = WWW(url)
        Yield(www)
        local noerror = not www.error or #(www.error) == 0
		local success = www.isDone and noerror
        if success then
        	if cb then cb(www.assetBundle.mainAsset) end
        	www.assetBundle:Unload(false)
        end
    end)
    coroutine.resume(c)
end

local function main()
	local uiRoot = createUIRoot()

	local url = CUtils.GetAssetFullPath("UILogin.u3d")

	loadRes(url,function (obj)
		local loginView = LuaHelper.Instantiate(obj)--Object.Instantiate("UILogin")
    	loginView:SetActive(true)
    	loginView.name = "UILogin"

    	loginView.transform:SetParent(uiRoot.transform);
        loginView.transform.localPosition = Vector3(0, 0, 0);
        loginView.transform.localScale = Vector3(1, 1, 1);
	end)

    --[[local req = LRequest(url)
    req.onCompleteFn = function(r)
    	warn("finished",r.data,r.assetBundle.mainAsset)
    	local loginView = LuaHelper.Instantiate(r.data)--Object.Instantiate("UILogin")
    	r.assetBundle:Unload(false)
    	loginView:SetActive(true)
    	loginView.name = "UILogin"

    	loginView.transform:SetParent(uiRoot.transform);
        loginView.transform.localPosition = Vector3(0, 0, 0);
        loginView.transform.localScale = Vector3(1, 1, 1);
	end
	LHighway.instance:LoadReq(req)]]
end

main()