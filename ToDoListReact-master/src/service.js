import axios from 'axios';

// const apiUrl = "http://localhost:5081";
const apiUrl =process.env.REACT_APP_API_URL;
export default {
  getTasks: async () => {
    console.log("getTasks");
    
    const result = await axios.get(`${apiUrl}/items`);
    console.log(result.data);
    
    return result.data;
  },

addTask: async(name)=>{
  const result = await axios.post(`${apiUrl}/items`,{
    name:name,
    isComplete:false
  })
  return result.data;
},



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
