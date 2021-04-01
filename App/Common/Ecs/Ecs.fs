namespace Common.Ecs

module Ecs =
    type IComponent =
        interface
        end

    type EntityId = EntityId of int

    type SystemUpdate =
        | UpdateEntities of (float -> Map<EntityId, List<IComponent>> -> Map<EntityId, List<IComponent>>)
        | UpdateWorld of (float -> World -> World)

    and Consumer = Consumer of (World -> unit)

    and World =
        { entities: Map<EntityId, List<IComponent>>
          systems: List<SystemUpdate>
          consumers: List<Consumer> }

    let concatMap a b =
        Map.fold (fun acc key value -> Map.add key value acc) a b

    let runSystem dt world system =
        let { entities = entities } = world

        match system with
        | UpdateEntities f ->
            { world with
                  entities = concatMap entities (f dt entities) }
        | UpdateWorld f -> f dt world

    let worldUpdate (dt: float) (world: World) =
        let newWorld =
            world.systems |> List.fold (runSystem dt) world

        newWorld.consumers
        |> List.iter (fun (Consumer f) -> f newWorld)

        newWorld
