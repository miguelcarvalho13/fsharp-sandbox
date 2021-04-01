namespace Common.Game.Systems

open Common.Ecs
open Common.Ecs.Component
open Common.Game.Components

module OutputSystem =
    let shapeToOutput shape position =
        match shape with
        | Circle s -> DrawCircleAt(position, s)
        | Rectangle s -> DrawRectangleAt(position, s)

    let getDrawables entities =
        entities
        |> Map.fold
            (fun drawables _ components ->
                components
                |> Helper.matchComponent2<Shape, Position>
                |> Option.map (fun (s, p) -> shapeToOutput s p)
                |> List.singleton
                |> List.append drawables)
            []
        |> List.choose id

    let updateComponents (drawables) (components: List<Ecs.IComponent>) =
        let newOutputSequence = OutputSequence(Clear :: drawables)

        components
        |> List.map
            (fun c ->
                match c with
                | :? OutputSequence -> newOutputSequence :> Ecs.IComponent
                | _ -> c)

    let run =
        Ecs.UpdateEntities
            (fun _ entities ->
                let drawables = getDrawables entities

                entities
                |> Map.map (fun _ components -> updateComponents drawables components))
