import json
import pickle
import pandas as pd

from textblob import TextBlob

from tensorflow.keras.models import load_model                      # type: ignore
from tensorflow.keras.preprocessing.text import Tokenizer           # type: ignore
from tensorflow.keras.preprocessing.text import tokenizer_from_json # type: ignore
from tensorflow.keras.preprocessing.sequence import pad_sequences   # type: ignore

#=== === === === === ===

class TheModels:

    # loading the tokenizer config and creating it
    tokenizer_config = json.load(open('saved_models/tokenizer_config.json'))
    tokenizer = tokenizer_from_json(tokenizer_config)
    print(f"Tokenizer loaded ...")

    # loading the model for scaling
    scaler = pickle.load(open('saved_models/scaler.pkl', 'rb'))
    print(f"Scaler loaded ...")

    # loading the model for prediction
    model = load_model('saved_models/model_03.h5')
    print(f"Model loaded ...")

    def __init__(self, value):
        self.instance_variable = value

#=== === === === === ===

def get_prediction(message):
    
    # pre-processing the messages
    X_text, X_features = _preprocess(message)
    
    # executing the model
    prediction = _execute_model(X_text, X_features)

    # returning the prediction
    return prediction[0][0]

#=== === === === === ===

def _preprocess(message):

    # converting message to array
    message_array = [message]
    print(f"Message array: {message_array}")
    
    # tokenizing the message
    X_text = TheModels.tokenizer.texts_to_sequences(message_array)
    print(f"Message tokenized: {X_text}")
    X_text = pad_sequences(X_text, maxlen=200)
    print(f"Message padded: {X_text}")

    #--- --- ---

    # getting the additional features
    polarity     = TextBlob(message).sentiment.polarity
    subjectivity = TextBlob(message).sentiment.subjectivity
    df_feats     = pd.DataFrame({'sentiment_polarity': [polarity], 'sentiment_subjectivity': [subjectivity], 'dominant_topic': [0]})
    feats_array  = df_feats[['sentiment_polarity', 'sentiment_subjectivity', 'dominant_topic']].values
    print(f"Features array: {feats_array}")

    #--- --- ---

    # normalizing/scaling the additional features
    X_features = TheModels.scaler.transform(feats_array)
    print(f"Features scaled: {X_features}")

    #--- --- ---

    # returning the pre-processed messages
    return X_text, X_features

#=== === === === === ===

def _execute_model(X_text, X_features):

    # executing the model and getting the prediction
    print(f"Prediction: starting ...")
    y_pred = TheModels.model.predict([X_text, X_features])
    print(f"Prediction: {y_pred}")

    # returning the prediction
    return y_pred

#=== === === === === ===