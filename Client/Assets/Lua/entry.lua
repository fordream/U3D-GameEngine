import "UnityEngine"

local function createUIRoot()
	print("create uiroot")
	local uiRoot = GameObject("Canvas(UIRoot)");
    uiRoot.layer = UnityEngine.LayerMask.NameToLayer("UI");
    local canvas = uiRoot:AddComponent("Canvas");
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
        local www = WWW(url)
        Yield(www)
        local noerror = not www.error or #(www.error) == 0
		local success = www.isDone and noerror
        if success then
        	if cb then cb(www.assetBundle:LoadAllAssets()[1]) end
        	www.assetBundle:Unload(false)
        else
            warn(("loadRes url:=%s,error:=%s"):format(url,www.error))
        end

        www:Dispose()
    end)
    coroutine.resume(c)
end

local function loadRes2(url,cb)
    local req = LRequest(url)
    req.onCompleteFn = function(r)
        warn("finished",r.data,r.assetBundle.mainAsset)
        if cb then cb(r.data) end
        r.assetBundle:Unload(false)
    end
    LHighway.instance:LoadReq(req)
end

local function main()
	local uiRoot = createUIRoot()

	local url = CUtils.GetAssetFullPath("UILogin.u3d")

	loadRes(url,function (obj)
		local loginView = obj:Instantiate("UILogin") --LuaHelper.Instantiate(obj)
    	loginView:SetActive(true)
    	loginView.name = "UILogin"

    	loginView.transform:SetParent(uiRoot.transform);
        loginView.transform.localPosition = Vector3(0, 0, 0);
        loginView.transform.localScale = Vector3(1, 1, 1);
	end)
end

main()