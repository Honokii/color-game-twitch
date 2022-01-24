namespace WIS.Utils.Helpers {
    public static class StringHelper {
        public static int GetInt(string stringVal) {
            if (int.TryParse(stringVal, out int result)) {
                return result;
            }

            return -1;
        }
    }
}