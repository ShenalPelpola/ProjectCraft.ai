import { useState } from 'react';
import { ChatMessageTypes } from '../../../Enums/ChatMessageTypes';
import { ChatItem } from '../ChatItem/ChatItem';
import { PromptTextBox } from '../PromptTextBox/PromptTextBox';
import { Empties } from '../../../Constants/Empties';
import { generateProject } from '../../../Operations/ProjectGeneration';
import { ProjectCreateWindow } from '../ProjectCreateWindow/ProjectCreateWindow';
import { ProjectStructureView } from '../ProjectStructureView/ProjectStructureView';
import './ChatView.scss';

export const ChatContainer = ({ isCreateWindowOpen, toggleCreateWindow }) => {
    const [conversationId, setConversationId] = useState(Empties.emptyString);
    const [history, setHistory] = useState(Empties.emptyArray);

    const onPromptSubmission = async (prompt) => {
        const data = await generateProject(prompt, conversationId);
        setHistory([...history, data["fileStructure"]]);
    };

    return (
        <div className="chat-container">
            <ProjectCreateWindow isOpen={isCreateWindowOpen} onClose={toggleCreateWindow}>Hello World</ProjectCreateWindow>
            <div className="chat-list">
                {history.map((historyItem) => {
                    return <ProjectStructureView data={historyItem} />
                })}
            </div>
            <div className="chat-footer">
                <PromptTextBox onSearchSubmit={onPromptSubmission} placeholder="Send you message here"></PromptTextBox>
            </div>
        </div>
    )
};