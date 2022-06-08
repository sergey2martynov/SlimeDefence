 _____         _    __     __    _                      _        _      
|  ___|_ _ ___| |_  \ \   / /__ | |_   _ _ __ ___   ___| |_ _ __(_) ___ 
| |_ / _` / __| __|  \ \ / / _ \| | | | | '_ ` _ \ / _ \ __| '__| |/ __|
|  _| (_| \__ \ |_    \ V / (_) | | |_| | | | | | |  __/ |_| |  | | (__ 
|_|  \__,_|___/\__|    \_/ \___/|_|\__,_|_| |_| |_|\___|\__|_|  |_|\___|
 ____  _       _       ____  _               _                          
| __ )| | ___ | |__   / ___|| |__   __ _  __| | _____      _____        
|  _ \| |/ _ \| '_ \  \___ \| '_ \ / _` |/ _` |/ _ \ \ /\ / / __|       
| |_) | | (_) | |_) |  ___) | | | | (_| | (_| | (_) \ V  V /\__ \       
|____/|_|\___/|_.__/  |____/|_| |_|\__,_|\__,_|\___/ \_/\_/ |___/                                                                                                                                                
  Paul Gerla, (c) 2021                               


-= Description =-

Fast Volumetric Blob Shadows is a simple shader-based solution intended for making
fast-to-render shadows. The cost to render is roughly similar to an unlit particle
with depth blending enabled. The cube or sphere-shaped shadow volumes can be moved,
rotated, and scaled as needed. It works in forward and deferred render pipelines,
and a version for the URP is included in a .unitypackage.
  
The primary advantage this asset has over the standard Unity projector is that the
projector will re-render all of the objects it affects, which can potentially
increase the number of draw calls and overall cost of a scene dramatically,
especially if there are multiple objects casting shadows on multiple environment 
objects, or the objects receiving the shadows are large or complex. The FVBS
shader doesn't suffer from this issue, and will have the same cost regardless of
the complexity of the scene being shadowed.
  
In the URP pipeline specifically one of the primary downsides to this approach is
that if the camera gets too close to the geometry, it will clip in and the shadow 
will disappear. For this reason I would not recommend this solution for large 
shadow volumes or ones that the camera will get especially close to. The builtin 
version of the shader does not suffer from this problem.
  
The builtin version was made with Amplify Shader Editor and can be modified with 
that package or with a text editor. The URP was made with the Unity Shader Graph.
  

-= Quickstart Guide =-
  
1) If using the URP version, delete the BlobShadow.shader file and then 
    extract the BlobShadowURP .unitypackage file.
2) Create a new material asset.
3) Set the shader of that material to be BlobShadow or Shader Graphs/
    BlobShadowGraph if using URP.
4) Place a cube or a sphere mesh into the scene. A low-poly sphere and cube mesh 
    without uvs or normals are included in the package, but an ordinary cube mesh 
    works just as well in the majority of cases.
5) Disable light probes, reflection probes, shadow casting, and shadow receiving on
    the mesh renderer.
6) Apply the material to the mesh.


-= Troubleshooting =-
  
If no shadow volumes show up at all, it may be that the depth texture is disabled on
the camera. For Builtin, try adding EnableDepthTextures.cs to the game object with 
the camera component on it. For URP, make sure Depth Texture is set to On either in
the camera component or the Pipeline Settings.

If a shadow volume is not visible in the scene, make sure that the mesh is
intersecting geometry in the scene that writes to the depth buffer (generally opaque
or alpha-tested). Also check to be sure that both Power and Intensity on the material
are greater than 0.

If the shadow volume is visible through another object and should not be, check to
ensure that the object you can see the shadow through is writing to the depth buffer.

If the shadow volume looks like it's clipping incorrectly, make sure your cube mesh
is 1 unit square -- or your sphere mesh is 1 unit in diameter -- at a scale of one.
Also check to ensure that the near clipping plane of the camera is not penetrating
the geometry of the shadow volume (in the URP version).

If you need to modify the material render queue of a standard URP shader, such as
Universal Render Pipeline/Lit, I've included a small script to allow that, it's under
Window/Rendering/Set Material Queue.


-= Material Properties Guide =-

Color - A simple color value for the shadow effect, can be an HDR color for other
    effects, such as a glow. The alpha channel of the color acts as a multiplier on the
    intensity, so it's easy to tune the intensity to a desired amount and be able to 
    easily fade the shadow in and out to that intensity.
  
Intensity - The is a multiplier on the strength of the effect, can be turned up past
    1 for very strong shadows.
  
Power - Turning this value up will expand the shadow out toward the edges, sharpening
    the edges and filling in the center.
  
Allow Shape Blending - Disabling this boolean will force the shader to only allow a
    spherical shape, improving render performance by a very small amount.
  
Cube to Sphere Blend - This slider will blend the shadow from a cube (0) shape to a
    sphere (1) shape.
    
Rounded Cube Bias - These next two values control the roundness of the corners of a
    cube-shaped shadow. The bias can generally be though of as the amount that the 
    corners are pushed inwards.

Rounded Cube Power - The power can be thought of as the width of the inward push of
    the corners. The two default values (bias of 2.5, power of 3.5) for these should
    work well in most cases.
	
	
-= Feedback =-

Email - paul@pixel-tea.com
Twitter - https://twitter.com/Pawige

Please feel free to contact me with questions, issues, requests, a project you used
this for, or just to say hi!