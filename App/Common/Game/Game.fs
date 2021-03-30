namespace Common.Game

open Common.Ecs
open Common.Game.Components
open Common.Game.Systems

module Game =
    let world: Ecs.World = {
        entities =
            Map.empty
            |> Map.add (Ecs.EntityId 1) [Position (0.0,0.0); Direction (1.0, 0.5); Speed 0.5]
        systems = [MovementSystem.run]
    }

    let run =
        Ecs.worldUpdate 0.016 world

