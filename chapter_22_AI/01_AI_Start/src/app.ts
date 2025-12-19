import express from 'express';
import { errorHandler } from '#middlewares';
import { geminiRouter, openAIrouter } from '#routes';

const app = express();
const port = process.env.PORT || 3000;

app.use(express.json());

// ROUTES
app.use('/openai', openAIrouter);
app.use('/gemini', geminiRouter);

app.use(errorHandler);

app.listen(port, () =>
  console.log(`\x1b[35m📡 Server is running at:http://localhost:${port}\x1b`)
);
