import * as _ from "lodash";
import { end, readAsLines, start } from "../helpers";

let { time, execution } = start([
  { file: "test", answer: 5 },
  { file: "input", answer: 1766 },
]);

let lines = readAsLines("08", execution);

enum CommandAction {
  NoOperation,
  Jump,
  Accumulator,
}

class Command {
  action: CommandAction;
  size: number;
  visited: boolean;
}

let commands: Command[] = lines.map((x) => {
  let actionString = x.split(" ")[0];

  let action: CommandAction = CommandAction.NoOperation;

  switch (actionString) {
    case "acc":
      action = CommandAction.Accumulator;
      break;
    case "jmp":
      action = CommandAction.Jump;
      break;
  }

  return {
    action: action,
    size: parseInt(x.substring(5), 10) * (x.substring(4, 5) === "+" ? 1 : -1),
    visited: false,
  };
});

function execute(
  currentCommands: Command[]
): { success: boolean; accumulator: number } {
  let accumulator = 0;

  for (let i = 0; i < currentCommands.length; i++) {
    let command = currentCommands[i];

    if (command.visited) {
      break;
    }

    switch (command.action) {
      case CommandAction.NoOperation:
        break;
      case CommandAction.Accumulator:
        accumulator += command.size;
        break;
      case CommandAction.Jump:
        i += command.size - 1;
        break;
    }

    command.visited = true;
  }

  return { success: true, accumulator };
}

let answer = execute(commands).accumulator;

end(time, answer, execution);
