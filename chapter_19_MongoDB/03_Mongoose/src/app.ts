import '#db';
import { Post, User } from '#models';

// CREATE
// const user = new User({
//   firstName: 'John',
//   lastName: 'Doe',
//   email: 'john.doe@example.com',
// });

// const existingUser = await User.findOne({ email: user.email });
// if (existingUser) throw new Error('Email already exists');

// const userToSave = await user.save();
// console.log('user created:', userToSave);

// const user2 = await User.create({
//   firstName: 'Jane',
//   lastName: 'Doe',
//   email: 'jane.doe@example.com',
// });

// console.log('user created:', user2);

// READ
const allUsers = await User.find();
// console.log(allUsers);

const findJoe = await User.find({ firstName: 'John' });
// console.log(findJoe);

const findById = await User.findById('692eb384ee54f8b2608eb0a7');
// console.log(findById);

// UPDATE
// const updateJohn = await User.updateOne(
//   { email: 'john.doe@example.com' },
//   { firstName: 'Jack' }
// );

// console.log(updateJohn);

// const userRole = await User.updateMany({ lastName: 'Doe' }, { role: 'admin' });
// console.log(userRole);

// const findByIdAndUpdate = await User.findByIdAndUpdate(
//   '692eb04b9a7842be5b8f7ee4',
//   { firstName: 'Updated Again' },
//   { new: true }
// );

// console.log(findByIdAndUpdate);

// const testing = await User.findOneAndUpdate(
//   { email: 'john.doe@example.com' },
//   { $set: { firstName: 'new Firstname' } },
//   { new: true, runValidators: true }
// );

// console.log(testing);

// DELETE

// const deleteOne = await User.deleteOne({ email: 'john.doe@example.com' });
// // console.log(deleteOne);

// const deleteMany = await User.deleteMany({ lastName: 'Doe' });
// console.log(deleteMany);

// const findByIdandDelete = await User.findByIdAndDelete(
//   '692eb836faaeef07342beb0e'
// );
// console.log(findByIdandDelete);

const user = await User.create({
  firstName: 'John',
  lastName: 'Doe',
  email: 'john.doe@example.com',
});

// console.log(user);

const newPost = await Post.create({
  title: 'My first Blog post',
  content: 'This is some content.....',
  author: user._id,
});

// console.log('Post created', newPost);

const postWithUser = await Post.findById(newPost._id).populate(
  'author',
  'firstName lastName email'
);
console.log(postWithUser);
