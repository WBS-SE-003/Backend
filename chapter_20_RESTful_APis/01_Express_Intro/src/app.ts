import { products } from './data/index.ts';
import express from 'express';

const app = express();

const port = 3000;

// USERS

app.get('/users', (req, res) => res.json({ message: 'GET all users' }));
app.post('/users', (req, res) => res.json({ message: 'Create new user' }));
app.get('/users/:id', (req, res) => res.json({ message: 'GET user by ID' }));
app.put('/users/:id', (req, res) => res.json({ message: 'UPDATE user by ID' }));
app.delete('/users/:id', (req, res) => res.json({ message: 'delete a user' }));

// POSTS
app.get('/posts', (req, res) => res.json({ message: 'GET all posts' }));
app.get('/posts/:id', (req, res) => res.json({ message: 'GET post by ID' }));
app.post('/posts', (req, res) => res.json({ message: 'create a new post' }));
app.put('/posts/:id', (req, res) =>
  res.json({ message: 'update a post by ID' })
);
app.delete('/posts/:id', (req, res) => res.json({ message: 'delete post' }));

app.get('/route', (req, res) => {
  // ...here
  // the logic
  // const users = await User.find();
  // res.json(users);
});

app.get('/products', (req, res) => res.json(products));

app.get('/products/:id', (req, res) => {
  // 1- get the parameter from the URL
  const { id } = req.params;
  //   console.log(id);

  // 2- Find the matching product in the list (id)
  const product = products.find((p) => p.id === Number(id));
  //   console.log(product);

  if (!product) return res.status(404).json({ mesasge: 'Product not found' });

  res.json(product);
});

app.listen(port, () =>
  console.log(`\x1b[35m📡 Server is running at http://localhost:${port}\x1b`)
);
