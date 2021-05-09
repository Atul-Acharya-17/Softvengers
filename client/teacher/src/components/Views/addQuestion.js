//unused component
import {useState} from 'react';
import Modal from 'react-modal';
import {Button, ModalBody, ModalHeader, Label, Row , Col } from 'reactstrap';
import { Control, LocalForm, Errors } from 'react-redux-form';

const AddQuestion = () => {
    const [wrongOptions, setWrongOptions] = useState();
    const [points, setPoints] = useState(0);
    const [universeID, setUniverseID] = useState(0);
    const [solarID, setsolarID] = useState(0);
    const [questionID, setQuestionID] = useState(0);
    const [body, setBody] = useState('');
    const [correct, setCorrect] = useState('');
    const [isModalOpen, setIsModalOpen] = useState(true);
    const required = (val) => val && val.length;
    const maxLength = (len) => (val) => !(val) || (val.length <= len);
    const minLength = (len) => (val) => (val) && (val.length >= len);

    function toggleModal(){
        setIsModalOpen(!isModalOpen);
    }
    function handleChange(){

    }

    function handleSubmit(event){
        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IlNVMDAxQGUubnR1LmVkdS5zZyIsImlkIjoiNjA1MzE2Njk5ZDRhNjI0MmYwZDk5M2RmIiwidHV0R3AiOiJTQ0U0IiwiaWF0IjoxNjE2MDU4MTg2fQ.7LFzy-ecqB89ZNydkPR0LhuM33SV3ciaPJmO_g9oQnc");

        var raw = {event};

        var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: raw,
        redirect: 'follow'
        };

        fetch("http://localhost:5000/teacher/question", requestOptions)
        .then(response => response.text())
        .then(result => console.log(result))
        .catch(error => console.log('error', error));
        console.log('logges');
    }
return(
    <Modal isOpen = {isModalOpen} toggle = {toggleModal()} >
            <ModalHeader toggle = {toggleModal()}>Add Question</ModalHeader>
            <ModalBody>
                {
                    <div className = "col-12 col-md-9" >
                    <LocalForm onSubmit = {(event) => handleSubmit(event)}>
                        <Row className = "form-group">
                            <Label htmlFor = "universe" md={10}> Select Universe ID </Label>
                            <Col md = {10}  >
                                <Control.select className = "col-12 col-md-9" model = ".universeID" id = "universeID" name = "universeID" 
                                onChange={(e)=> setUniverseID(e.target.value)}>
                                    <option value = "0">0</option>
                                    <option value = "1">1</option>
                                    <option value = "2">2</option>
                                    <option value = "3">3</option>
                                    <option value = "4">4</option>
                                </Control.select>
                            </Col>
                        </Row>
                        <Row className = "form-group">
                            <Label htmlFor = "body" md={3}> Question Body </Label>
                            <Col md = {10}>
                                <Control.textarea className = "col-12 col-md-9"  model = ".body" id = "body" name = "body" 
                                rows = "6"
                                className = "form-control"
                                placeholder = "enter question body"
                                onChange={(e)=> setBody(e.target.value)}
                                />
                            </Col>
                        </Row>
                        <Row className = "form-group">
                            <Label htmlFor = "correct" md={9}> Correct Option</Label>
                            <Col md = {10}>
                                <Control.text className = "col-12 col-md-9" model = ".correct" id = "correct" name = "correct" 
                                placeholder = "enter qcorrect option"
                                className = "form-control"
                                validators = {
                                    {
                                        required, minLength: minLength(3), maxLength:maxLength(15)
                                    }
                                    }
                                onChange={(e)=> setCorrect(e.target.value)}
                               />
                            <Errors 
                                className = "text-danger"
                                model = ".name"
                                show = "touched"
                                messages = {
                                    {
                                        required: 'required',
                                        minLength: 'must be greater than 2 char ',
                                        maxLength: 'must be less than or equal to 15 characters'
                                    }
                                }
                            />  
                            </Col>
                        </Row>
                        <Row className = "form-group">
                            <Col md = {{size:10, offset:2}}>
                                <Button type = "submit" color = "primary">
                                    Submit
                                </Button>
                            </Col>
                        </Row>
                    </LocalForm>
                    
                </div>
                }
            </ModalBody>
            </Modal>
)
}
export default AddQuestion