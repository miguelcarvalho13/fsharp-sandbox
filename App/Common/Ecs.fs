module Ecs

type Component =
    | Speed of float
    | Position of int * int
    | HP of int

type EntityId = EntityId of int


type SystemUpdate =
    | Update of (Map<EntityId, List<Component>> -> Map<EntityId, List<Component>>)

type World = {
    entities: Map<EntityId, List<Component>>
    systems: List<SystemUpdate>
}


let concatMap a b = Map.fold (fun acc key value -> Map.add key value acc) a b;


let runSystem entities system =
    match system with
    | Update f -> concatMap entities (f entities)


let worldUpdate (world: World) =
    { world with
        entities = List.fold runSystem world.entities world.systems }
