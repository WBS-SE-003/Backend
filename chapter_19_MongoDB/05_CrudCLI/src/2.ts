import { Command } from 'commander';

const program = new Command();

//prettier-ignore
program
    .name('Commander setup')
    .description('commander setup')
    .version('1.0.0')
    .argument('<name>', 'Your name')
    .action((name) => {
        console.log(`Hello 👋🏻, ${name}! Welcome to the CLI`)
    })

program.parse();
