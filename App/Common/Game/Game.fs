namespace Common.Game

open Common.Ecs
open Common.Game.Components
open Common.Game.Systems

module Game =
    let initWorld : Ecs.World =
        { entities =
              Map.empty
              |> Map.add
                  (Ecs.EntityId 1)
                  [ Circle { radius = 3.0 }
                    Position(5.0, 5.0)
                    Direction(1.0, 0.5)
                    Speed 1.0
                    OutputSequence [] ]
          systems = [ MovementSystem.run; OutputSystem.run ]
          consumers = [] }

    let run dt world = Ecs.worldUpdate dt world
