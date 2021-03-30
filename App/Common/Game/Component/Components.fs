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
