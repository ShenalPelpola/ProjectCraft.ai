import PropTypes from 'prop-types';
import { ChatMessageTypes } from '../../../Enums/ChatMessageTypes'
import { Empties } from '../../../Constants/Empties';
import UserIcon from '../../../assets/user-icon.png';
import AiIcon from '../../../assets/chatgptLogo.svg';
import './ChatItem.scss';

export const ChatItem = ({ chatMessageType, chatMessage }) => {
    return (
        <div className={`chat-item ${chatMessageType === ChatMessageTypes.Ai && 'ai-chat-item'}`}>
            <img src={chatMessageType === ChatMessageTypes.Human ? UserIcon : AiIcon} alt="chat-logo" />
            <p className="chat-message">
                {chatMessage}
            </p>
        </div>
    )
};

ChatItem.propTypes = {
    /* Type of the chat messge if human or ai*/
    chatMessageType: PropTypes.oneOf(Object.values(ChatMessageTypes)).isRequired,
    /* Chat message to display */
    chatMessage: PropTypes.string
};

ChatItem.defaultProps = {
    chatMessageType: ChatMessageTypes.Human,
    chatMessage: Empties.emptyString
};