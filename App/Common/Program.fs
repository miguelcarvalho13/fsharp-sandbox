// Learn more about F# at http://fsharp.org

open System
open System.Threading

open Common.Ecs
open Common.Ecs.Component
open Common.Game
open Common.Game.Components


let processOutputs (world: Ecs.World) =
    let outputs =
        world.entities
        |> Map.fold
            (fun c _ components ->
                match c with
                | Some _ -> c
                | None -> Helper.matchComponent<OutputSequence> components)
            None

    match outputs with
    | Some (OutputSequence os) ->
        os
        |> List.fold
            (fun (s: string array array) o ->
                match o with
                | Clear ->
                    Console.Clear()
                    s
                | DrawCircleAt (Position (xf, yf), { radius = rf }) ->
                    let centerX = Convert.ToInt32 xf
                    let centerY = Convert.ToInt32 yf
                    let r = Convert.ToInt32 rf

                    Console.WriteLine ("Circle at (" + (string centerX) + "," + (string centerY) + ") with r = " + (string r) + "\n")

                    [(centerX - r)..(centerX + r)]
                    |> List.iter (fun x ->
                        [(centerY - r)..(centerY + r)]
                        |> List.iter (fun y ->
                            let dx = x - centerX
                            let dy = y - centerY

                            if (dx * dx + dy * dy < r * r) then
                                Array.set s.[y] x "."
                                ()
                            else
                                ()
                            )
                        )

                    s
                | DrawRectangleAt (Position (x, y), { width = w; height = h }) -> s)
            ([| 0 .. 20 |]
             |> Array.map (fun _ -> [| 0 .. 80 |] |> Array.map (fun _ -> " ")))
        |> Array.map (fun x -> x |> Array.fold (+) "" |> (+) "\n")
        |> Array.fold (+) ""
        |> Console.WriteLine
    | None -> ()

let sleep (t: int) x =
    Thread.Sleep t
    x

[<EntryPoint>]
let rec main argv =
    printfn "Hello World from F#!"

    let t = 32

    [ 1 .. 500 ]
    |> List.fold
        (fun w _ -> w |> Game.run (float t / 1000.0) |> sleep t)
        { Game.initWorld with
              consumers = [ Ecs.Consumer processOutputs ] }
    |> ignore

    0 // return an integer exit code
