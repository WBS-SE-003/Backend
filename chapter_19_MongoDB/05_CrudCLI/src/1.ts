// console.log(process.argv[0]);
// console.log(process.argv[1]);
console.log(process.argv[2]);
console.log(process.argv[3]);

const num1 = Number(process.argv[2]);
const num2 = Number(process.argv[3]);

function multiply(a: number, b: number) {
  return a * b;
}

console.log(`Result: ${multiply(num1, num2)}`);
