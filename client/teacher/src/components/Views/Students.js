const Students = ({students}) => {
    
    return (
       <>{students.map((student)=> (<h3 key={student.id}>{student.name}</h3>))}</>
    )
}

export default Students
