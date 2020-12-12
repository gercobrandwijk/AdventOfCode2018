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
    { file: "test", answer: 286 },
    { file: "input", answer: 18107 },
  ],
  false
);

let lines = readAsLines("12", execution);

let instructions = lines.map((x) => ({
  type: x.substring(0, 1) as InstructionType,
  value: parseInt(x.substring(1), 10),
}));

let waypoint: Position = { x: 10, y: 1 };
let position: Position = { x: 0, y: 0 };
let direction: Direction = Direction.East;

for (let instruction of instructions) {
  switch (instruction.type) {
    case InstructionType.N:
      waypoint.y += instruction.value;
      break;
    case InstructionType.E:
      waypoint.x += instruction.value;
      break;
    case InstructionType.S:
      waypoint.y -= instruction.value;
      break;
    case InstructionType.W:
      waypoint.x -= instruction.value;
      break;
    case InstructionType.L:
      let amountRotateLeft = instruction.value % 360;

      while (amountRotateLeft > 0) {
        let newX = waypoint.y * -1;
        let newY = waypoint.x;

        waypoint.x = newX;
        waypoint.y = newY;

        direction = directionAddDegrees(direction, 90);

        amountRotateLeft -= 90;
      }
      break;
    case InstructionType.R:
      let amountRotateRight = instruction.value % 360;

      while (amountRotateRight > 0) {
        let newX = waypoint.y;
        let newY = waypoint.x * -1;

        waypoint.x = newX;
        waypoint.y = newY;

        direction = directionAddDegrees(direction, 90);

        amountRotateRight -= 90;
      }

      break;
    case InstructionType.F:
      position.x += waypoint.x * instruction.value;
      position.y += waypoint.y * instruction.value;
      break;
  }
}

let answer = Math.abs(position.x) + Math.abs(position.y);

end(time, answer, execution);
