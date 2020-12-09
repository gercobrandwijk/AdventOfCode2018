import * as fs from "fs";
import * as _ from "lodash";
import { xor } from "lodash";
import { endExecution, readAsLines, startExecution } from "../helpers";

let time = startExecution();

let day = "08";

//let lines = readAsLines('test', day);
let lines = readAsLines("input", day);

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

function reset(currentCommands: Command[]) {
  currentCommands.forEach((x) => (x.visited = false));
}

function switchAction(currentCommand: Command) {
  switch (currentCommand.action) {
    case CommandAction.NoOperation:
      currentCommand.action = CommandAction.Jump;
      break;
    case CommandAction.Jump:
      currentCommand.action = CommandAction.NoOperation;
      break;
  }
}

function execute(
  currentCommands: Command[]
): { success: boolean; accumulator: number } {
  let accumulator = 0;

  for (let i = 0; i < currentCommands.length; i++) {
    let command = currentCommands[i];

    if (command.visited) {
      return { success: false, accumulator };
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

execute(commands);

let activePathIndexes = commands
  .map((item, index) => ({ visited: item.visited, action: item.action, index }))
  .filter((item) => item.visited && item.action !== CommandAction.Accumulator)
  .map((item) => item.index);

reset(commands);

let answer = null;

for (let index of activePathIndexes) {
  let currentCommands: Command[] = commands;

  switchAction(currentCommands[index]);

  let executeResult = execute(currentCommands);

  if (executeResult.success) {
    answer = executeResult.accumulator;

    break;
  }

  reset(currentCommands);
  switchAction(currentCommands[index]);
}

endExecution(time, answer, 1639);
