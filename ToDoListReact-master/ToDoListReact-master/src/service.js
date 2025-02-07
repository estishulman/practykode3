import axios from 'axios';

const apiUrl = "http://localhost:5081";

export default {
  getTasks: async () => {
    console.log("getTasks");
    
    const result = await axios.get(`${apiUrl}/items`);
    console.log(result.data);
    
    return result.data;
  },
//   addTask: async (name) => {
//     console.log("addTask");
//     console.log(name);
    
//     const newTask = { 
//         Name: name,           // שם המשימה
//         IsComplete: false     // מצב ההשלמה (ברירת מחדל לא שלם)
//     };
    
//     const result = await axios.post(`${apiUrl}/items`, newTask); // שלח את האובייקט
//     return result.data; // מחזיר את המשימה שנוספה
// },


// addTask: async (name) => {
//   console.log("addTask");
//   console.log(name);
  
  

//   try {
//       const result = await axios.post(`${apiUrl}/items/?name=${name}` )
         
//       return result.data; 
//   } catch (error) {
//       console.error("Error adding task:", error);
//       throw error; // זרוק את השגיאה כדי לטפל בה במקום אחר אם צריך
//   }
// },

addTask: async(name)=>{
  const result = await axios.post(`${apiUrl}/items`,{
    name:name,
    isComplete:false
  })
  return result.data;
},

  // setCompleted: async (idItems, IsComplete) => {
  //   console.log('setCompleted', { idItems, IsComplete });
  //   const updatedTask = { IsComplete: IsComplete }; // אובייקט עם המידע המעודכן
  //   await axios.put(`${apiUrl}/items/${idItems}`, IsComplete);
  //   return {}; // מחזיר אובייקט ריק או את המידע המעודכן אם תרצה
  // },

  // setCompleted: async (idItems, IsComplete) => {
    
  //   try {

  //     const response = await axios.put(`${apiUrl}/items/`, idItems, IsComplete); // שים לב לשם המאפיין
  //     return response.data;
  //   } catch (error) {
  //     console.error("Error updating completion status:", error);
  //     throw error; // זרוק את השגיאה כדי לתפוס אותה במקום אחר
  //   }
  // },

  setCompleted: async (idItems, IsComplete) => {
    try {
        const response = await axios.put(`${apiUrl}/items/${idItems}?IsComplete=${IsComplete}`); // שלח את IsComplete כפרמטר ב-URL
        return response.data;
    } catch (error) {
        console.error("Error updating completion status:", error);
        throw error; // זרוק את השגיאה כדי לתפוס אותה במקום אחר
    }
},


  deleteTask: async (idItems) => {

    console.log('deleteTask', idItems);
    await axios.delete(`${apiUrl}/items/${idItems}`);
    return {}; // מחזיר אובייקט ריק או מידע נוסף אם תרצה
  }
};
