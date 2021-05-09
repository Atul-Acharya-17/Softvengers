import React, {useState} from 'react';
import { produce } from 'immer';
import {generate} from 'shortid';
import Modal from 'react-modal';
import {Button} from 'reactstrap';
import { StringLiteralLike } from 'typescript';

interface QuestionObject {
    
    points: number,
    questionID: number,
    body: string,
    correctOption: string
    wrongOptions: Array<String>
}

const Rough = () => {
    const [arr, setArr] = useState([0,1,2])
    const [questions, setQuestions] = useState<QuestionObject []>([
        {
      
        points: 0,
        questionID: 0,
        body: "",
        correctOption: "",
        wrongOptions:[]}
    ]);
    // const [isModalOpen, setIsModalOpen] = useState(false);
    // const setModalIsOpentoTrue = () => {
    //     setIsModalOpen(true);
    // }
    // const setModalIsOpentoFalse = () => {
    //     setIsModalOpen(false);
    // }

    return(
        <>
            {/* <Button onClick = {setModalIsOpentoTrue}> Add Question </Button>
            <Modal isOpen = {isModalOpen} ariaHideApp={false}> */}
                    <div style = {{textAlign: "center"}}>
                    
                    <button onClick = {()=> {
                        setQuestions(currQuestions=> [
                            ...currQuestions,
                            {
                                
                                wrongOptions: [],
                            points: 0,
                            questionID: 0,
                            body: "",
                            correctOption: ""
                            }
                        ])
                    }}> Add New Assignment </button>
                    {questions.map((q,index) =>{
                        return(
                            <div key = {q.questionID}>
                                <input onChange = {(e)=>{
                                    arr.map(i => {
                                        const tempOption = e.target.value;
                                        setQuestions(currQues => 
                                            produce(currQues,(v)=>{
                                               
                                                    v[index].wrongOptions[i] = tempOption;
                                               
                                                
                                            })
                                            ); 
                                    })
                                    
                                }}
                                placeholder = "wrong options"></input>
                                <input onChange = {(e)=>{
                                    const points = e.target.value;
                                    setQuestions(currPoint => 
                                        produce(currPoint,(v)=>{
                                            v[index].points = Number(points);
                                        })
                                        );
                                }}
                                
                                placeholder = "Points"/>
                                
                               
                                <input placeholder = "question ID"/>
                                <input placeholder = "question body"/>
                                <input placeholder = "correct option"/>
                            </div>
                        )
                    })}
                    <div>
                    {JSON.stringify(questions)}
                    </div>
                </div>
         {/* </Modal> */}
        </>     
    );
};
export default Rough;