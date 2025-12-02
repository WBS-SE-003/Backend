import mongoose from 'mongoose';

// const MONGO_URI = process.env.MONGO_URI!;

try {
  const con = await mongoose.connect(process.env.MONGO_URI!);
  console.log('✅ connected to MongoDB');
  console.log(`📂 Using DB: ${con.connection.name}`);
} catch (error) {
  console.error('❌ MongoDB conection error', error);
  process.exit(1);
}
