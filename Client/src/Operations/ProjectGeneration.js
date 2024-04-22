import axios from 'axios'

export async function generateProject(prompt, conversationId) {
    const data = {
        prompt,
        conversationId
    };

    try {
        const response = await axios.post('http://localhost:5000/api/project/generate', data);
        return response.data;
    } 
    catch (error) {
        return error;
    }
}