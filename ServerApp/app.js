const express = require("express");
const db = require('./database/sqlite');

const app = express();
app.use(express.json());
const apiRouter = express.Router();

const financeRouter = express.Router();
financeRouter.get('/getFinances', async (req, res) => {
    const id = Number(req.query['id']);
    db.all(`SELECT * FROM finances WHERE id = (?)`, [id], (err, rows) => {
        if (err) {
            return res.status(500).send(err.message);
        }
        res.json(rows);
    })
});

apiRouter.use('/finances', financeRouter);
app.use('/api', apiRouter);

app.listen(3000);