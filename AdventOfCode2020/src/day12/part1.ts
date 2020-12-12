import * as _ from "lodash";
import { end, readAsLines, start } from "../helpers";
import {
  Direction,
  directionAddDegrees,
  InstructionType,
  Position,
} from "./shared";

let { time, execution } = start(
  [
    { file: "test", answer: 25 },
    { file: "input", answer: 879 },
  ],
  false
);

let lines = readAsLines("12", execution);

let instructions = lines.map((x) => ({
  type: x.substring(0, 1) as InstructionType,
  value: parseInt(x.substring(1), 10),
}));

let position: Position = { x: 0, y: 0 };
let direction: Direction = Direction.East;

for (let instruction of instructions) {
  switch (instruction.type) {
    case InstructionType.N:
      position.y -= instruction.value;
      break;
    case InstructionType.E:
      position.x += instruction.value;
      break;
    case InstructionType.S:
      position.y += instruction.value;
      break;
    case InstructionType.W:
      position.x -= instruction.value;
      break;
    case InstructionType.L:
      direction = directionAddDegrees(direction, -instruction.value);
      break;
    case InstructionType.R:
      direction = directionAddDegrees(direction, instruction.value);
      break;
    case InstructionType.F:
      switch (direction) {
        case Direction.North:
          position.y -= instruction.value;
          break;
        case Direction.East:
          position.x += instruction.value;
          break;
        case Direction.South:
          position.y += instruction.value;
          break;
        case Direction.West:
          position.x -= instruction.value;
          break;
      }
      break;
  }
}

let answer = Math.abs(position.x) + Math.abs(position.y);

end(time, answer, execution);
