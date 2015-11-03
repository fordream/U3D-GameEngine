import "UnityEngine"
require "preload"

local function createUIRoot()
	print("create uiroot")
	local uiCanvas = GameObject("UIRoot(2D)");
    uiCanvas.transform.localPosition = Vector3(0, 0, 0);
    uiCanvas.transform.localScale = Vector3(1, 1, 1);
    uiCanvas.layer = LayerMask.NameToLayer("UI");
    local canvas = uiCanvas:AddComponent("Canvas");
    canvas.renderMode = UnityEngine.RenderMode.ScreenSpaceCamera;

    uiCanvas:AddComponent(UI.CanvasScaler);
    uiCanvas:AddComponent(UI.GraphicRaycaster);

    local eventObj = GameObject("EventSystem");
    local eventSystem = eventObj:AddComponent(EventSystems.EventSystem);
    eventObj:AddComponent(EventSystems.StandaloneInputModule);
    eventObj:AddComponent(EventSystems.TouchInputModule);

    local camobj = GameObject("UICamera")
    camobj.layer = LayerMask.NameToLayer("UI");
    local cam = camobj:AddComponent("Camera")
    camobj.transform:SetParent(uiCanvas.transform)
    camobj.transform.localPosition = Vector3(0, 0, 0);
    camobj.transform.localScale = Vector3(1, 1, 1);
    --cam.clearFlags = CameraClearFlags.Color
    cam.clearFlags = CameraClearFlags.Depth
    --cam.backgroundColor = Color(128,128,128,255)
    cam.cullingMask = 32
    cam.orthographicSize = 1;
    cam.orthographic = true;
    cam.nearClipPlane = -10;
    cam.farClipPlane = 1000;

    canvas.worldCamera = cam

    return uiCanvas
end
local function CreateMainCamera()
    local cam_root = GameObject("Main Camera Root")
    cam_root.transform.localPosition = Vector3(0, 0, 0);
    cam_root.transform.localScale = Vector3(1, 1, 1);
    local camobj = GameObject("Main Camera")
    camobj:AddComponent("Animation")
    camobj.tag = "MainCamera"
    local cam = camobj:AddComponent("Camera")
    cam.backgroundColor = Color.black
    cam.nearClipPlane = 0.2
    cam.farClipPlane = 1000
    camobj.transform:SetParent(cam_root.transform)
    camobj.transform.localPosition = Vector3(0, 0, 0);
    camobj.transform.localScale = Vector3(1, 1, 1);
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
        local console = obj:Instantiate("UIConsole") --LuaHelper.Instantiate(obj)
        console:SetActive(true)
        console.name = "UIConsole"

        console.transform:SetParent(root.transform);
        console.transform:SetAsLastSibling()
        console.transform.localPosition = Vector3(0, 0, 0);
        console.transform.localScale = Vector3(1, 1, 1);
        
        --[[/*Left*/ rectTransform.offsetMin.x;
        /*Right*/ rectTransform.offsetMax.x;
        /*Top*/ rectTransform.offsetMax.y;
        /*Bottom*/ rectTransform.offsetMin.y;]]

        local trans = console:GetComponent("RectTransform");
        -- trans.sizeDelta = Vector2(5,5)
        -- trans.anchoredPosition3D = Vector3(5,5,0)
        -- trans.anchoredPosition = Vector2(5,5)
        trans.offsetMax = Vector2(-5,-5)
        trans.offsetMin = Vector2(5,5)

        if cb then cb() end
    end)
end

local function main()
    CreateMainCamera()
	local uiCanvas = createUIRoot()

	loadConsole(uiCanvas)
    showLogin(uiCanvas)
end

main()