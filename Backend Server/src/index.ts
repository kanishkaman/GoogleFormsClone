import express from 'express';
import bodyParser from 'body-parser';
import fs from 'fs';
import path from 'path';

const app = express();
const PORT = 3000;
const DB_FILE = path.join(__dirname, '..', 'db.json');

app.use(bodyParser.json());

app.get('/ping', (req, res) => {
  res.json(true);
});

app.post('/submit', (req, res) => {
  const { name, email, phone, github_link, stopwatch_time } = req.body;
  const submission = { name, email, phone, github_link, stopwatch_time };

  const db = JSON.parse(fs.readFileSync(DB_FILE, 'utf8'));
  db.submissions.push(submission);
  fs.writeFileSync(DB_FILE, JSON.stringify(db));

  res.status(200).json({ message: 'Submission saved successfully!' });
});

app.get('/read', (req, res) => {
  const index = parseInt(req.query.index as string);
  const db = JSON.parse(fs.readFileSync(DB_FILE, 'utf8'));

  if (index >= 0 && index < db.submissions.length) {
    res.status(200).json(db.submissions[index]);
  } else {
    res.status(404).json({ message: 'Submission not found!' });
  }
});

app.listen(PORT, () => {
  console.log(`Server is running on http://localhost:${PORT}`);
});

app.delete('/delete', (req, res) => {
  const index = req.query.index as string | undefined;
  const db = JSON.parse(fs.readFileSync(DB_FILE, 'utf8'));

  if (index && parseInt(index) >= 0 && parseInt(index) < db.submissions.length) {
    db.submissions.splice(parseInt(index), 1);
    fs.writeFileSync(DB_FILE, JSON.stringify(db));
    res.status(200).json({ message: 'Submission deleted successfully!' });
  } else {
    res.status(404).json({ message: 'Submission not found!' });
  }
});

