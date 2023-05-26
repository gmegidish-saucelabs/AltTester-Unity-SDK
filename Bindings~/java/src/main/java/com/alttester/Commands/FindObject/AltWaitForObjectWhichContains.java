/*
    Copyright(C) 2023  Altom Consulting

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

package com.alttester.Commands.FindObject;

import com.alttester.Utils;
import com.alttester.IMessageHandler;
import com.alttester.AltObject;
import com.alttester.altTesterExceptions.WaitTimeOutException;

/**
 * Wait until it finds an object that respect the given criteria or times run
 * out and will throw an error.
 */
public class AltWaitForObjectWhichContains extends AltBaseFindObject {

    /**
     * @param altWaitForObjectsParameters the properties parameter for finding the
     *                                    objects in a scene.
     */
    private AltWaitForObjectsParams altWaitForObjectsParameters;

    public AltWaitForObjectWhichContains(IMessageHandler messageHandler,
            AltWaitForObjectsParams altWaitForObjectsParameters) {
        super(messageHandler);
        this.altWaitForObjectsParameters = altWaitForObjectsParameters;
        this.altWaitForObjectsParameters.setCommandName("findObject");
    }

    public AltObject Execute() throws WaitTimeOutException {
        double time = 0;
        AltObject altElement = null;
        while (time < altWaitForObjectsParameters.getTimeout()) {
            logger.debug("Waiting for element where name contains "
                    + altWaitForObjectsParameters.getAltFindObjectsParameters().getPath() + "....");
            try {
                altElement = new AltFindObjectWhichContains(messageHandler,
                        altWaitForObjectsParameters.getAltFindObjectsParameters()).Execute();
                if (altElement != null) {
                    return altElement;
                }
            } catch (Exception e) {
                logger.warn("Exception thrown: " + e.getLocalizedMessage());
            }
            Utils.sleepFor(altWaitForObjectsParameters.getInterval());
            time += altWaitForObjectsParameters.getInterval();
        }
        throw new WaitTimeOutException("Element " + altWaitForObjectsParameters.getAltFindObjectsParameters().getPath()
                + " still not found after " + altWaitForObjectsParameters.getTimeout() + " seconds");
    }
}
