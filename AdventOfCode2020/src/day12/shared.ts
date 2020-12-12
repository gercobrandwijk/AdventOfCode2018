export enum Direction {
  North,
  East,
  South,
  West,
}

export class Position {
  x: number;
  y: number;
}

export enum InstructionType {
  N = "N",
  E = "E",
  S = "S",
  W = "W",
  L = "L",
  R = "R",
  F = "F",
}

export class Instruction {
  type: InstructionType;
  value: number;
}

export function directionAddDegrees(
  currentDirection: Direction,
  addDegrees: number
): Direction {
  let newDegrees;

  switch (currentDirection) {
    case Direction.North:
      newDegrees = 0;
      break;
    case Direction.East:
      newDegrees = 90;
      break;
    case Direction.South:
      newDegrees = 180;
      break;
    case Direction.West:
      newDegrees = 270;
      break;
  }

  newDegrees += addDegrees;

  newDegrees = newDegrees % 360;

  switch (newDegrees) {
    case 0:
      return Direction.North;
    case 90:
    case -270:
      return Direction.East;
    case 180:
    case -180:
      return Direction.South;
    case 270:
    case -90:
      return Direction.West;
  }

  throw new Error("Degrees " + newDegrees + " is not supported");
}

