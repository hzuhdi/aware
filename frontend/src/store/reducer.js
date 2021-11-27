import { combineReducers } from 'redux';

// reducer import
import customizationReducer from './customizationReducer';

const reducer = combineReducers({
    customization: customizationReducer
});

export default reducer;
