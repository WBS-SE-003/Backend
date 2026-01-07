const car = {
  brand: 'Toyota',
  model: 'Corolla',
  year: 2020,

  drive() {
    console.log('the car is driving...');
  },

  stop() {
    console.log('the car has stopped');
  },
};

// car.drive();
// car.stop();

const robot = {
  name: 'Robo',
  model: 'RX-X1',
  year: 2026,

  greet: function () {
    console.log(
      `Hello, i am ${this.name}, model ${this.model}, built in ${this.year}`
    );
  },

  performTask: function (task) {
    console.log(`${this.name} is performing task: ${task}`);
  },
};

robot.greet();
robot.performTask('cleaning');

let value = 5; // number
console.log(typeof value);
value = 'five'; // string
console.log(typeof value);
value = true;
console.log(typeof value);

function multiply(a, b) {
  return a * b;
}

console.log(multiply(10, 10));
console.log(multiply(10, '10'));

// in TS
// function multiply2(a: number, b: number): number {
//   return a * b;
// }

// console.log(multiply2(10, 10));
// console.log(multiply2(10, '10'));
