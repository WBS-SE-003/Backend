// post => should accept GET and POST
// posts/:id => should accept GET, PUT & DELETE

import http, { type RequestListener } from 'node:http';

const createResponse = (
  res: http.ServerResponse,
  statusCode: number,
  message: unknown
) => {
  res.writeHead(statusCode, { 'content-type': 'application/json' });
  return res.end(
    typeof message === 'string'
      ? JSON.stringify({ message })
      : JSON.stringify(message)
  );
};

const requestHandler: RequestListener = (req, res) => {
  const { method, url } = req;

  if (url === '/posts') {
    if (method === 'GET')
      return createResponse(res, 200, 'GET request on /posts');
    if (method === 'POST')
      return createResponse(res, 201, 'POST request on /posts');
    return createResponse(res, 405, 'Method is not allowed');
  }

  if (url?.startsWith('/posts/')) {
    if (method === 'GET')
      return createResponse(res, 200, `GET Request on ${url}`);
    if (method === 'PUT')
      return createResponse(res, 200, `PUT request on ${url}`);
    if (method === 'DELETE')
      createResponse(res, 200, `Delete request on ${url}`);
    return createResponse(res, 405, 'Method is not allowed');
  }
};

const port = 3000;
const server = http.createServer(requestHandler);

server.listen(port, () =>
  console.log(`Server is running at http://localhost:${port}`)
);
