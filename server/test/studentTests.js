
const chai = require('chai')

const chaiHttp = require('chai-http');
const { response } = require('express');

const server = require('../index');

const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IkFSQVRSSUtBMDAxQGUubnR1LmVkdS5zZyIsImlkIjoiNjA2NmYxMjA1ODYwNDQyNzY4YjA4YzMxIiwidHV0R3AiOiJTQ0U1IiwiaWF0IjoxNjE5MjQwOTM4fQ.gBE5K-gR7lwLD1etJ95Cq8pjkrg7L0DA1Oi4NQzrhAg";

chai.should();

chai.use(chaiHttp);

describe('Student API',()=>{

    // 
    // Test the POST login route
    // 

    describe('POST /student/login',()=>{
        it ("Successful Login", async()=>{

            const login = {
                emailID:"aratrika001@e.ntu.edu.sg",
                password: "U1922069F"
            }

            chai.request(server)
                .post("/student/login")
                .send(login)
                .end((err, response)=>{
                    response.should.have.status(200);
                    response.body.should.be.a('object');
                    response.body.should.have.property('message');
                    response.body.should.have.property('token');
                    response.body.should.have.property('message').eq('True');
                    response.body.should.have.property('token').a('String');
                    //done();
                })

        })

        it ("Unuccessful Login - Password Incorrect", async()=>{

            const login = {
                emailID:"aratrika001@e.ntu.edu.sg",
                password: "U1922069"
            }

            chai.request(server)
                .post("/student/login")
                .send(login)
                .end((err, response)=>{
                    response.should.have.status(404);
                    response.body.should.be.a('object');
                    response.body.should.have.property('message');
                    response.body.should.have.property('token');
                    response.body.should.have.property('message').eq('False');
                    response.body.should.have.property('token').a('array');
                    //done();
                })
            
        })

        it ("Unuccessful Login - Username Incorrect", async()=>{

            const login = {
                emailID:"aratrika00@e.ntu.edu.sg",
                password: "U1922069F"
            }

            chai.request(server)
                .post("/student/login")
                .send(login)
                .end((err, response)=>{
                    response.should.have.status(404);
                    response.body.should.be.a('object');
                    response.body.should.have.property('message');
                    response.body.should.have.property('token');
                    response.body.should.have.property('message').eq('False');
                    response.body.should.have.property('token').a('array');
                    //done();
                })
            
        })


    })
    describe('GET /student/details/getLeaderboard', ()=>{
        it ("Sorted Leaderboard", async()=>{

            chai.request(server)
                .get("/student/details/getLeaderboard")
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(200);
                    response.body.should.be.a('object');
                    response.body.should.have.property('students');

                    response.body.should.have.property('myRank');
                    response.body.should.have.property('students').a('array');
                    response.body.should.have.property('myRank').a('number');
                    //done();
                })

        })

        it ("Bad request", async()=>{

            chai.request(server)
                .post("/student/details/getLeaderboard")
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(404);
                    response.text.should.be.eq('Resource not found');
                })

        })

    })

    describe('GET /student/details/getAllStudents/:tut_grp', ()=>{
        it ("Tutorial Group exists", async()=>{

            chai.request(server)
                .get("/student/details/getAllStudents/SCE4")
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(200);
                    response.body.should.be.a('array');
                })

        })

        it ("Tutorial Group does not exist", async()=>{

            chai.request(server)
                .get("/student/details/getAllStudents/SCE1000")
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(200);
                    response.text.should.be.eq('[]');
                })

        })

        it ("Bad request", async()=>{

            chai.request(server)
                .post("/student/details/getAllStudents/SCE4")
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(404);
                    response.text.should.be.eq('Resource not found');
                })

        })

    })

    describe('GET /student/challenge/getSentChallenges', ()=>{
        it ("Student exists", async()=>{

            chai.request(server)
                .get("/student/challenge/getSentChallenges")
                .query({emailID: "ARATRIKA001@e.ntu.edu.sg"})
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(200);
                    response.body.should.be.a('array');
                })

        })

        it ("Student does not exist", async()=>{

            chai.request(server)
                .get("/student/challenge/getSentChallenges")
                .query({emailID: "ARATRIKA00@e.ntu.edu.sg"})
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    
                    response.should.have.status(200);
                    response.text.should.be.eq('[]');
                })

        })

        it ("Bad request", async()=>{

            chai.request(server)
                .post("/student/challenge/getSentChallenges?emailID=ARATRIKA001@e.ntu.edu.sg")
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(404);
                    response.text.should.be.eq('Resource not found');
                })

        })

    })

    describe('GET /student/challenge/getReceivedChallenges', ()=>{
        it ("Student exists", async()=>{

            chai.request(server)
                .get("/student/challenge/getReceivedChallenges")
                .query({emailID: "ARATRIKA001@e.ntu.edu.sg"})
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(200);
                    response.body.should.be.a('array');
                })

        })

        it ("Student does not exist", async()=>{

            chai.request(server)
                .get("/student/challenge/getReceivedChallenges")
                .query({emailID: "ARATRIKA00@e.ntu.edu.sg"})
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    
                    response.should.have.status(200);
                    response.text.should.be.eq('[]');
                })

        })

        it ("Bad request", async()=>{

            chai.request(server)
                .post("/student/challenge/getReceivedChallenges?emailID=ARATRIKA001@e.ntu.edu.sg")
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(404);
                    response.text.should.be.eq('Resource not found');
                })

        })

    })

    describe('GET /student/details/getAllStudents', ()=>{
        it ("Get all students", async()=>{

            chai.request(server)
                .get("/student/details/getAllStudents")
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(200);
                    response.body.should.be.a('array');
                })

        })

        it ("Bad request", async()=>{

            chai.request(server)
                .post("/student/details/getAllStudents")
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(404);
                    response.text.should.be.eq('Resource not found');
                })

        })

    })

    describe('GET /student/posts', ()=>{
        it ("Get all posts", async()=>{

            chai.request(server)
                .get("/student/posts/getPosts")
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(200);
                    response.body.should.be.a('array');
                })

        })

        it ("Bad request", async()=>{

            chai.request(server)
                .post("/student/posts/getPosts")
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(404);
                    response.text.should.be.eq('Resource not found');
                })

        })

    })

    describe('GET /student/details/getStudent', ()=>{
        it ("Student exists", async()=>{

            chai.request(server)
                .get("/student/details/getStudent")
                .set("Authorization", "Bearer "+token)
                .query({emailID: "ARATRIKA001@e.ntu.edu.sg"})
                .end((err, response)=>{
                    response.should.have.status(200);
                    response.body.should.be.a('object');
                    response.body.should.have.property('conqueredUniverse').a('number');
                    response.body.should.have.property('conqueredSolarSystem').a('number');
                    response.body.should.have.property('conqueredPlanet').a('number');
                    response.body.should.have.property('emailID');
                    response.body.should.have.property('totalScore').a('number');
                    response.body.should.have.property('matricNo');
                })

        })

        it ("Student does not exist", async()=>{

            chai.request(server)
                .get("/student/details/getStudent")
                .set("Authorization", "Bearer "+token)
                .query({emailID: "ARATRIKA00@e.ntu.edu.sg"})
                .end((err, response)=>{
                    response.should.have.status(404);
                    response.body.should.have.property('message').eq('Student does not exist in DB');
                })

        })

    })

    describe('GET /student/questions', ()=>{
        it ("Topic exists", async()=>{

            chai.request(server)
                .get("/student/questions/")
                .query({universe:0, solarSystem: 1})
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    response.should.have.status(200);
                    response.body.should.be.have.property('easy').a('array');
                    response.body.should.be.have.property('medium').a('array');
                    response.body.should.be.have.property('hard').a('array');
                    response.body.should.be.have.property('easy').length(10);
                    response.body.should.be.have.property('medium').length(10);
                    response.body.should.be.have.property('hard').length(10);
                })

        })

        it ("Topic does not exist", async()=>{

            chai.request(server)
                .get("/student/questions/")
                .query({universe:100, solarSystem: 1})
                .set("Authorization", "Bearer "+token)
                .end((err, response)=>{
                    
                    response.should.have.status(200);
                    response.body.should.be.have.property('easy').length(0);
                    response.body.should.be.have.property('medium').length(0);
                    response.body.should.be.have.property('hard').length(0);
                })

        })


    })

    describe('JWT Authentication', ()=>{
        it ("No header - Unauthorized", async()=>{

            chai.request(server)
                .get("/student/posts/getPosts")
                .end((err, response)=>{
                    response.should.have.status(401);
                    response.text.should.be.eq('Unauthorized');
                })

        })

        it ("Incorrect token", async()=>{

            chai.request(server)
                .get("/student/posts/getPosts")
                .set("Authorization", "Bearer "+token+"f")
                .end((err, response)=>{
                    response.should.have.status(403);
                    response.text.should.be.eq('Forbidden');
                })

        })

    })
})