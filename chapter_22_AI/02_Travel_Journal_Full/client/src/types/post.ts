export interface Author {
  _id?: string;
  username?: string;
  firstName?: string;
  lastName?: string;
}

export interface Post {
  _id: string;
  title: string;
  image: string;
  content: string;
  author: Author;
  createdAt: string;
}
