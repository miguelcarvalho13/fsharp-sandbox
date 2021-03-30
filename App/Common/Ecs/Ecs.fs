namespace Common.Ecs

module Ecs =
    type IComponent =
        interface
        end

    type EntityId = EntityId of int

    type SystemUpdate = Update of (float -> Map<EntityId, List<IComponent>> -> Map<EntityId, List<IComponent>>)

    type World =
        { entities: Map<EntityId, List<IComponent>>
          systems: List<SystemUpdate> }

    let concatMap a b =
        Map.fold (fun acc key value -> Map.add key value acc) a b

    let runSystem dt entities system =
        match system with
        | Update f -> concatMap entities (f dt entities)

    let worldUpdate (dt: float) (world: World) =
        { world with
              entities = List.fold (runSystem dt) world.entities world.systems }
