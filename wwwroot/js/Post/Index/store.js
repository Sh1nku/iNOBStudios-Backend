var store = new Vuex.Store({
    strict: true,
    state: {
        post: null,
    },
    mutations: {
        setModel: (state, value) => {
            state.post = value;
        },
    },
    actions: {
        init: (context) => {
            context.commit('setModel', model);
        },
    }
});