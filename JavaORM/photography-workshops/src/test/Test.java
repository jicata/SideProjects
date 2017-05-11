import org.junit.Assert;
import org.junit.Test;

public class T00TestClassExists {

    // Error messages
    private static final String CLASS_NOT_PRESENT_ERROR_MESSAGE = "whatever error bro";

    // Class names
    private static final String[] classNames = new String[] {
            "Car",
            "ShowCar",
            "PerformanceCar",
            "Garage",
            "Race",
            "CasualRace",
            "DragRace",
            "DriftRace"
    };

    @Test
    public void test() {
        assertExistingClasses(classNames);
    }

    private void assertExistingClasses(String[] classNames) {
        for (String className : classNames) {
            assertClassExists(className);
        }
    }

    private void assertClassExists(String className) {
        Assert.assertTrue(String.format(CLASS_NOT_PRESENT_ERROR_MESSAGE, className),
                Classes.allClasses.containsKey(className));
    }
}
