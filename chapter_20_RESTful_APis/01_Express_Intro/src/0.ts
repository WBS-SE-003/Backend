import express from 'express';

const app = express();

const port = 3000;

// app.get('/', (req, res) => res.send('hello?'));
// app.get('/something', (req, res) => res.send('this is another endpoint'));

// app.get('/logger', (req, res) => {
//   const { url, method } = req;
//   //   console.log(req);

//   res.json({
//     message: `a ${method} request was sent to ${url}`,
//   });
// });

// app.get('/statuscode', (req, res) => {
//   res.status(418).json({ message: 'check your insomnia' });
// });

app.listen(port, () =>
  console.log(`Server is running at http://localhost:${port}`)
);
