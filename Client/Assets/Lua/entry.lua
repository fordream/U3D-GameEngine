import "UnityEngine"
require "preload"

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

    uiRoot.transform.localPosition = Vector3(0, 0, -10);
    uiRoot.transform.localScale = Vector3(1, 1, 1);
    return uiRoot
end
local function CreateMainCamera()
    local cam_root = GameObject("Main Camera Root")
    local camobj = GameObject("Main Camera")
    camobj:AddComponent("Animation")
    camobj.tag = "MainCamera"
    local cam = camobj:AddComponent("Camera")
    cam.backgroundColor = Color.black
    cam.nearClipPlane = 0.2
    cam.farClipPlane = 1000
    --cam.cullingMask = _G.default_cull_mask_pre
    camobj.transform:SetParent(cam_root.transform)

    return camobj
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

local function showLogin(root,cb)
    local url = CUtils.GetAssetFullPath("UILogin.u3d")

    loadRes(url,function (obj)
        local loginView = obj:Instantiate("UILogin") --LuaHelper.Instantiate(obj)
        loginView:SetActive(true)
        loginView.name = "UILogin"

        loginView.transform:SetParent(root.transform);
        loginView.transform.localPosition = Vector3(0, 0, 0);
        loginView.transform.localScale = Vector3(1, 1, 1);

        if cb then cb() end
    end)
end

local function loadConsole(root,cb)
    local url = CUtils.GetAssetFullPath("console.u3d")
    loadRes(url,function (obj)
        local console = obj:Instantiate("DebugConsole") --LuaHelper.Instantiate(obj)
        console:SetActive(true)
        console.name = "DebugConsole"

        console.transform:SetParent(root.transform);
        
        local trans = console:GetComponent("RectTransform");
        trans.anchoredPosition3D = Vector3(5,5,0)
        trans.anchoredPosition = Vector2(5,5)
        trans.sizeDelta = Vector2(5,200)

        if cb then cb() end
    end)
end

local function main()
    CreateMainCamera()
	local uiRoot = createUIRoot()

	loadConsole(uiRoot,function()
        showLogin(uiRoot)
    end)
end

main()