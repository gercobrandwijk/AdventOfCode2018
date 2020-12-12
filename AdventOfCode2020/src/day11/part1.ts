import * as _ from "lodash";
import { end, readAsLines, start } from "../helpers";
import { fillMatrix, Place, PlaceState, printMatrix } from "./shared";

let { time, execution } = start(
  [
    { file: "test", answer: 37 },
    { file: "input", answer: 2438 },
  ],
  false
);

let lines = readAsLines("11", execution);

let matrix: Place[][] = fillMatrix(lines);

function determine() {
  matrix.forEach((row, rowIndex) => {
    row.forEach((place, colIndex) => {
      if (place.state === PlaceState.Floor) return;

      place.reset();

      // Places above
      if (rowIndex > 0) {
        // Left
        if (colIndex > 0) {
          if (
            matrix[rowIndex - 1][colIndex - 1].state === PlaceState.Occupied
          ) {
            place.aroundOccupied++;
          }
        }

        // Same
        if (matrix[rowIndex - 1][colIndex].state === PlaceState.Occupied) {
          place.aroundOccupied++;
        }

        // Right
        if (colIndex < row.length - 1) {
          if (
            matrix[rowIndex - 1][colIndex + 1].state === PlaceState.Occupied
          ) {
            place.aroundOccupied++;
          }
        }
      }

      // Left
      if (colIndex > 0) {
        if (matrix[rowIndex][colIndex - 1].state === PlaceState.Occupied) {
          place.aroundOccupied++;
        }
      }

      // Right
      if (colIndex < row.length - 1) {
        if (matrix[rowIndex][colIndex + 1].state === PlaceState.Occupied) {
          place.aroundOccupied++;
        }
      }

      // Places below
      if (rowIndex < matrix.length - 1) {
        // Left
        if (colIndex > 0) {
          if (
            matrix[rowIndex + 1][colIndex - 1].state === PlaceState.Occupied
          ) {
            place.aroundOccupied++;
          }
        }

        // Same
        if (matrix[rowIndex + 1][colIndex].state === PlaceState.Occupied) {
          place.aroundOccupied++;
        }

        // Right
        if (colIndex < row.length - 1) {
          if (
            matrix[rowIndex + 1][colIndex + 1].state === PlaceState.Occupied
          ) {
            place.aroundOccupied++;
          }
        }
      }
    });
  });
}

function rules() {
  let changeAmount = 0;

  matrix.forEach((row) => {
    row.forEach((place) => {
      switch (place.state) {
        case PlaceState.Available:
          if (place.aroundOccupied === 0) {
            place.state = PlaceState.Occupied;
            changeAmount++;
          }
          break;
        case PlaceState.Occupied:
          if (place.aroundOccupied >= 4) {
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
