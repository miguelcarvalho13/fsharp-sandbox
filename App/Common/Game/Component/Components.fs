namespace Common.Game.Components

open Common.Ecs

type Speed =
    | Speed of float
    interface Ecs.IComponent

type Position =
    | Position of float * float
    interface Ecs.IComponent

type Direction =
    | Direction of float * float
    interface Ecs.IComponent

type CircleShape = { radius: float }

type RectangleShape = { width: float; height: float }

type Shape =
    | Circle of CircleShape
    | Rectangle of RectangleShape
    interface Ecs.IComponent


type Output =
    | Clear
    | DrawCircleAt of Position * CircleShape
    | DrawRectangleAt of Position * RectangleShape

type OutputSequence =
    | OutputSequence of List<Output>
    interface Ecs.IComponent
