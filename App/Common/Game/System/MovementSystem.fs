namespace Common.Game.Systems

open Common.Ecs
open Common.Ecs.Component
open Common.Game.Components

module MovementSystem =
    let queryComponents components =
        components
        |> Helper.matchComponent3<Position, Direction, Speed>

    let updateComponents dt (components: List<Ecs.IComponent>) tuple : List<Ecs.IComponent> =
        match tuple with
        | Some (Position (x, y), Direction (dx, dy), Speed s) ->
            components
            |> List.map
                (fun c ->
                    match c with
                    | :? Position -> (Position(x + (dx * s * dt), y + (dy * s * dt)) :> Ecs.IComponent)
                    | _ -> c)
        | None -> components

    let run =
        Ecs.UpdateEntities
            (fun dt entities ->
                entities
                |> Map.map
                    (fun _ components ->
                        components
                        |> queryComponents
                        |> updateComponents dt components))
