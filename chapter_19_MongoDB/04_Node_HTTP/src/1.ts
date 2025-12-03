import http from 'node:http';

const server = http.createServer((req, res) => {
  res.statusCode = 418;
  //   res.setHeader('Content-Type', 'text/plain');
  //   res.end('Hello');

  res.setHeader('Content-Type', 'application/json');
  res.end(JSON.stringify({ message: 'Hello again!' }));

  //   res.setHeader('Content-Type', 'text/html');
  //   res.end('<h1>Hello</h1>');
});

const port = 3000;

server.listen(port, () =>
  console.log(`Server is runnning at http://localhost:${port}`)
);
