# Semi Fixed Time Step Interpolation
![Preview](preview.gif)  
It's a technique that allows the developer to have the game's calculations running in fixed time slots, while the rendering can occur whenever he wants.
This allows for things like client side prediction, deterministic game/match simulation (while on same platform) for feature testing and performance profiling and other benefits.
> Implementation based on https://developer.valvesoftware.com/wiki/Source_Multiplayer_Networking#Entity_interpolation.  

# Instructions
1. Opens the `Main` scene.
2. Uses WASD to move the real entity.
3. See both entity and interpolated entity.
4. To change real game ticking rate, goes to `Project Settings` -> `Time` -> `Fixed Timestep` 0.05ms = 20 updates per second since 1s / 0.05ms = 20.
5. `Buffer Memory` setting is how much in seconds a position at a time can be memoized inside the buffer.
6. `Interpolation Window` is how much in seconds in the past the interpolation will happen, the less the better, but in some cases like in a networking environment with client to server input loss, this value should be a little bigger or even, variable.
7. `Interpolation Window` should never be bigger or equal to the `Buffer Memory`. 
