import mongoose from 'mongoose';

try {
  const client = await mongoose.connect(process.env.MONGO_URI!);
  console.log(`👌🏻 connected to MongoDB`);
  console.log(`📦 using db: ${client.connection.name}`);
} catch (error) {
  console.error('MongoDB connection error', error);
  process.exit(1);
}
