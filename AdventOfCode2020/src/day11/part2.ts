import * as _ from "lodash";
import { end, readAsLines, start } from "../helpers";
import { fillMatrix, Place, PlaceState, printMatrix } from "./shared";

let { time, execution } = start(
  [
    { file: "test", answer: 26 },
    { file: "input", answer: 2174 },
  ],
  false
);

let lines = readAsLines("11", execution);

let matrix: Place[][] = fillMatrix(lines);

function determineOccupationInDirection(
  rowIndex: number,
  colIndex: number,
  directionRow: number,
  directionCol: number
): boolean {
  let checkRow = rowIndex;
  let checkCol = colIndex;

  let occupied = false;

  checkRow += directionRow;
  checkCol += directionCol;

  while (
    checkRow >= 0 &&
    checkRow < matrix.length &&
    checkCol >= 0 &&
    checkCol < matrix[0].length
  ) {
    if (matrix[checkRow][checkCol].state === PlaceState.Occupied) {
      occupied = true;
      break;
    }

    if (matrix[checkRow][checkCol].state === PlaceState.Available) {
      break;
    }

    checkRow += directionRow;
    checkCol += directionCol;
  }

  return occupied;
}

function determine() {
  matrix.forEach((row, rowIndex) => {
    row.forEach((place, colIndex) => {
      if (place.state === PlaceState.Floor) return;

      place.reset();

      place.occupiedUpLeft = determineOccupationInDirection(rowIndex, colIndex, -1, -1);
      place.occupiedUp = determineOccupationInDirection(rowIndex, colIndex, -1, 0);
      place.occupiedUpRight = determineOccupationInDirection(rowIndex, colIndex, -1, 1);

      place.occupiedLeft = determineOccupationInDirection(rowIndex, colIndex, 0, -1);
      place.occupiedRight = determineOccupationInDirection(rowIndex, colIndex, 0, 1);

      place.occupiedDownLeft = determineOccupationInDirection(rowIndex, colIndex, 1, -1);
      place.occupiedDown = determineOccupationInDirection(rowIndex, colIndex, 1, 0);
      place.occupiedDownRight = determineOccupationInDirection(rowIndex, colIndex, 1, 1);
    });
  });
}

function rules() {
  let changeAmount = 0;

  matrix.forEach((row) => {
    row.forEach((place) => {
      switch (place.state) {
        case PlaceState.Available:
          if (place.sumOccupied() === 0) {
            place.state = PlaceState.Occupied;
            changeAmount++;
          }
          break;
        case PlaceState.Occupied:
          if (place.sumOccupied() >= 5) {
            place.state = PlaceState.Available;
            changeAmount++;
          }
          break;
      }
    });
  });

  return changeAmount;
}

let changeAmount = 0;

do {
  determine();

  changeAmount = rules();

  //printMatrix(matrix);
} while (changeAmount > 0);

let answer = matrix.reduce(
  (sum, row) => sum + row.filter((x) => x.state === PlaceState.Occupied).length,
  0
);

end(time, answer, execution);
