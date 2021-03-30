namespace Common.Ecs.Component

open Common.Ecs

module Helper =
    let traverseTuple2 t =
        match t with
        | (Some a, Some b) -> Some(a, b)
        | _ -> None

    let traverseTuple3 t =
        match t with
        | (Some a, Some b, Some c) -> Some(a, b, c)
        | _ -> None

    let traverseTuple4 t =
        match t with
        | (Some a, Some b, Some c, Some d) -> Some(a, b, c, d)
        | _ -> None

    let traverseTuple5 t =
        match t with
        | (Some a, Some b, Some c, Some d, Some e) -> Some(a, b, c, d, e)
        | _ -> None

    let matchComponent<'a> (components: List<Ecs.IComponent>) =
        components |> List.tryFind (fun c -> box c :? 'a)

    let matchComponent2<'a, 'b> (components: List<Ecs.IComponent>) =
        components
        |> List.fold
            (fun cs c ->
                let (c1, c2) = cs

                match box c with
                | :? 'a as a -> (Some a, c2)
                | :? 'b as b -> (c1, Some b)
                | _ -> cs)
            (None, None)
        |> traverseTuple2

    let matchComponent3<'a, 'b, 'c> (components: List<Ecs.IComponent>) =
        components
        |> List.fold
            (fun cs c ->
                let (c1, c2, c3) = cs

                match box c with
                | :? 'a as a -> (Some a, c2, c3)
                | :? 'b as b -> (c1, Some b, c3)
                | :? 'c as c -> (c1, c2, Some c)
                | _ -> cs)
            (None, None, None)
        |> traverseTuple3

    let matchComponent4<'a, 'b, 'c, 'd> (components: List<Ecs.IComponent>) =
        components
        |> List.fold
            (fun cs c ->
                let (c1, c2, c3, c4) = cs

                match box c with
                | :? 'a as a -> (Some a, c2, c3, c4)
                | :? 'b as b -> (c1, Some b, c3, c4)
                | :? 'c as c -> (c1, c2, Some c, c4)
                | :? 'd as d -> (c1, c2, c3, Some d)
                | _ -> cs)
            (None, None, None, None)
        |> traverseTuple4

    let matchComponent5<'a, 'b, 'c, 'd, 'e> (components: List<Ecs.IComponent>) =
        components
        |> List.fold
            (fun cs c ->
                let (c1, c2, c3, c4, c5) = cs

                match box c with
                | :? 'a as a -> (Some a, c2, c3, c4, c5)
                | :? 'b as b -> (c1, Some b, c3, c4, c5)
                | :? 'c as c -> (c1, c2, Some c, c4, c5)
                | :? 'd as d -> (c1, c2, c3, Some d, c5)
                | :? 'e as e -> (c1, c2, c3, c4, Some e)
                | _ -> cs)
            (None, None, None, None, None)
        |> traverseTuple5
